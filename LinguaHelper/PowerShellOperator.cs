using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace LinguaHelper
{
    internal class PowerShellOperator
    {
        // <summary>
        /// The static powershell instance.
        /// </summary>
        private readonly PowerShell _powerShell;

        public PowerShellOperator()
        {
            _powerShell = PowerShell.Create();
            if (_powerShell == null) { throw new ArgumentException("Can't create a powershell instance"); }
            ExecuteCommand("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope Process");

        }

        public Collection<PSObject> ExecuteCommand(string command)
        {
            _powerShell.Commands.Clear();
            _powerShell.AddScript(command);
            var output = _powerShell.Invoke();
            HandleErrors();
            return output;
        }

        /// <summary>
        /// Throws an exception if the powershell instance had errors.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private  void HandleErrors()
        {
            if (_powerShell.Streams.Error.Count > 0)
            {
                // Handle errors
                StringBuilder msg = new StringBuilder();
                foreach (var error in _powerShell.Streams.Error)
                {
                    msg.AppendLine(error.Exception.ToString());
                }
                _powerShell.Streams.Error.Clear();

                throw new InvalidOperationException(msg.ToString());
            }
        }
    }
}
