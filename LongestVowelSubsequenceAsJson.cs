using System.Text.Json;

/// <summary>
/// Finds the longest consecutive vowel subsequence in each word and returns the results as JSON.
/// Returns an array of objects containing word, sequence, and length properties.
/// </summary>
/// <param name="words">List of words to analyze for vowel subsequences</param>
/// <returns>JSON string containing array of results with word, sequence, and length</returns>
static string LongestVowelSubsequenceAsJson(List<string>? words)
{
    // Handle null or empty input for edge cases
    if (words == null || words.Count == 0)
        return JsonSerializer.Serialize(new List<object>());

    // Define vowel characters for matching
    var vowels = new HashSet<char> {'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};
    var results = new List<object>();

    // Process each word to find longest vowel sequence
    foreach (var word in words)
    {
        string longestSequence = "";
        string currentSequence = "";

        // Scan each character in the word
        foreach (char c in word)
        {
            if (vowels.Contains(c))
            {
                // Build consecutive vowel sequence
                currentSequence += c;
            }
            else
            {
                // Non-vowel breaks sequence - check if current is longest
                if (currentSequence.Length > longestSequence.Length)
                {
                    longestSequence = currentSequence;
                }
                currentSequence = "";
            }
        }

        // Check final sequence (word might end with vowels)
        if (currentSequence.Length > longestSequence.Length)
        {
            longestSequence = currentSequence;
        }

        // Add result for this word
        results.Add(new
        {
            word = word,
            sequence = longestSequence,
            length = longestSequence.Length
        });
    }

    // Return properly serialized JSON
    return JsonSerializer.Serialize(results);
}
