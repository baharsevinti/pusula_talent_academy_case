using System.Text.Json;

static string MaxIncreasingSubArrayAsJson(List<int>? numbers)
{
    // Handle edge cases
    if (numbers == null || numbers.Count == 0) return "[]";
    if (numbers.Count == 1) return $"[{numbers[0]}]";

    List<int> bestSubarray = new List<int>();
    int bestSum = int.MinValue;
    
    List<int> currentSubarray = new List<int> { numbers[0] };
    int currentSum = numbers[0];

    for (int i = 1; i < numbers.Count; i++)
    {
        // Continue increasing sequence
        if (numbers[i] > numbers[i - 1])
        {
            currentSubarray.Add(numbers[i]);
            currentSum += numbers[i];
        }
        // Sequence broken, evaluate current
        else 
        {
            if (currentSubarray.Count > 1 && currentSum > bestSum)
            {
                bestSubarray = new List<int>(currentSubarray);
                bestSum = currentSum;
            }
            
            currentSubarray = new List<int> { numbers[i] };
            currentSum = numbers[i];
        }
    }

    // Evaluate final sequence
    if (currentSubarray.Count > 1 && currentSum > bestSum)
    {
        bestSubarray = new List<int>(currentSubarray);
    }

    // Return largest single element if no increasing sequence found
    if (bestSubarray.Count == 0)
    {
        bestSubarray = new List<int> { numbers.Max() };
    }

    return JsonSerializer.Serialize(bestSubarray);
}