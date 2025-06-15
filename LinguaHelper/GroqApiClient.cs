using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class GroqApiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private const string Endpoint = "https://api.groq.com/openai/v1/chat/completions";
    private const string Model = "llama-3.3-70b-versatile";

    public GroqApiClient(string apiKey)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    /// <summary>
    /// Sends an English text snippet to the Groq API for explanation in Russian,
    /// with support for cancellation of the HTTP request.
    /// </summary>
    /// <param name="englishText">
    /// The English text to be explained. Must not be null, empty, or whitespace.
    /// </param>
    /// <param name="cancellationToken">
    /// Token to observe while waiting for the task to complete, allowing the request to be cancelled.
    /// </param>
    /// <returns>
    /// A <see cref="string"/> containing the explanation in Russian, or an empty string if the API response was empty.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if <paramref name="englishText"/> is null, empty, or consists only of whitespace.
    /// </exception>
    /// <exception cref="HttpRequestException">
    /// Thrown if the HTTP response indicates a non-success status code.
    /// </exception>
    public async Task<string> ExplainEnglishTextInRussianAsync(
        string englishText,
        CancellationToken cancellationToken = default)
    {
        // Validate input before attempting network I/O
        if (string.IsNullOrWhiteSpace(englishText))
            throw new ArgumentException(
                "Input text cannot be empty or whitespace.",
                nameof(englishText));

        // Construct the request payload in accordance with OpenAI Chat Completions API schema
        var requestBody = new
        {
            model = Model,
            messages = new[]
            {
            new
            {
                role = "system",
                content = "Ты учитель английского языка. Тебе даётся текст на английском. " +
                          "Объясни его смысл. Пояснения давай на русском. Примеры, транскрипции на английском."
            },
            new
            {
                role = "user",
                content = englishText
            }
        },
            temperature = 0.7  // Controls randomness: lower values yield more focused output
        };

        // Serialize payload to JSON
        var json = JsonSerializer.Serialize(requestBody);

        // Wrap JSON into HTTP content, setting appropriate headers
        using var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        // Execute HTTP POST to the Groq API endpoint, observing cancellation
        using var response = await _httpClient
            .PostAsync(Endpoint, content, cancellationToken)
            .ConfigureAwait(false);

        // Throw if the API call was not successful (status code 200–299)
        response.EnsureSuccessStatusCode();

        // Read the response payload as a string, observing cancellation
        var responseJson = await response
            .Content
            .ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        // Parse the JSON response into a DOM for safe traversal
        using var doc = JsonDocument.Parse(responseJson);

        // Navigate to the content of the first choice in the completion
        var explanation = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        // Return the explanation or an empty string if null
        return explanation ?? string.Empty;
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
