using System;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinguaHelper
{
    internal class PowerShellOperator
    {
        /// <summary>
        /// The static powershell instance.
        /// </summary>
        private readonly PowerShell _powerShell;

        private PowerShellOperator()
        {
            _powerShell = PowerShell.Create();
        }

        public static async Task<PowerShellOperator> CreateAsync()
        {
            var instance = new PowerShellOperator();
            await instance.SetExecutionPolicyAsync();
            return instance;
        }

        /// <summary>
        /// Cancels the current PowerShell operation. Warning: cancelling the operation will cause the PowerShell instance to throw an exception!
        /// </summary>
        public void CancelCurrentOperation()
        {
            try
            {
                _powerShell.Stop();
            }
            catch (Exception)
            {
                // Handle or log the exception as necessary
                // For example: throw new InvalidOperationException("Error cancelling PowerShell operation.", ex);
            }
        }

        /// <summary>
        /// Set the execution policy of the powershell instance.
        /// </summary>
        /// <returns></returns>
        private async Task SetExecutionPolicyAsync()
        {
            await ExecuteCommandAsync("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process");
        }

        /// <summary>
        /// Semaphore to ensure that only one command is executed at a time.
        /// </summary>
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Executes the given command asynchronously and returns the result.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<PSDataCollection<PSObject>> ExecuteCommandAsync(string command)
        {
            // Ensure that only one command is executed at a time.
            await _semaphoreSlim.WaitAsync();
            try
            {
                _powerShell.Commands.Clear();
                _powerShell.AddScript(command);

                var asyncResult = _powerShell.BeginInvoke();
                var result = await Task<PSDataCollection<PSObject>>.Factory.FromAsync(asyncResult, _ => _powerShell.EndInvoke(asyncResult));

                HandleErrors();
                return result;
            }
            finally
            {
                // Release the semaphore.
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Throws an exception if the powershell instance had errors.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void HandleErrors()
        {
            if (_powerShell.Streams.Error.Count > 0)
            {
                var msg = new StringBuilder();
                foreach (var error in _powerShell.Streams.Error)
                {
                    msg.AppendLine(error.Exception.ToString());
                }
                _powerShell.Streams.Error.Clear();

                throw new InvalidOperationException($"Handling inner power shell errors: {msg}");
            }
        }
    }


}
