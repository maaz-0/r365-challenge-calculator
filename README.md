# String Calculator Challenge

A .NET Core console application that performs string calculations based on various input formats.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

## Project Structure

- `ChallengeCalculator.Console`: Console application project
- `ChallengeCalculator.Core`: Core business logic
- `ChallengeCalculator.Tests`: Unit tests

## How to Run

1. Clone the repository:
```bash
git clone [repository-url]
cd r365-challenge-calculator
```

2. Build the solution:
```bash
dotnet build
```

3. Run the console application:
```bash
# Run with default settings (negative numbers not allowed)
cd ChallengeCalculator.Console
dotnet run

# Run with a custom alternate delimiter (instead of \n)
dotnet run --alt-delimiter=|

# Run with negative numbers allowed
dotnet run --allow-negative

# Run with both options
dotnet run --alt-delimiter=| --allow-negative
```

## Command-line Arguments

The application supports the following command-line arguments:

- `--alt-delimiter=<character>`: Specify an alternate delimiter to use instead of '\n'
  ```bash
  # Example using | as the alternate delimiter
  dotnet run --alt-delimiter=|
  # Input: 1|2,3|4 will be treated as "1\n2,3\n4"
  ```

- `--allow-negative`: Enable support for negative numbers in calculations
  ```bash
  # Example with negative numbers enabled
  dotnet run --allow-negative
  # Input: 1,-2,3,-4 will return -2 (1 + -2 + 3 + -4)
  ```

Arguments can be combined:
```bash
# Use | as delimiter and enable negative numbers
dotnet run --alt-delimiter=| --allow-negative
# Input: 1|-2|3 will return 2 (1 + -2 + 3)
```

## Input Formats

The calculator supports various input formats:

1. Basic format with comma delimiter:
```
1,2,3,4
```

2. Numbers with newline delimiter (or alternate delimiter if specified):
```
1\n2,3     # With default delimiter
1|2,3      # With alternate delimiter |
```

3. Custom single-character delimiter:
```
//#\n2#5
```

4. Custom delimiter of any length:
```
//[***]\n11***22***33
```

5. Multiple custom delimiters:
```
//[*][!!][r9r]\n11r9r22*33!!44
```

## Rules

- Numbers greater than 1000 are ignored
- Negative numbers:
  - Not allowed by default
  - Can be enabled with --allow-negative flag
- Invalid numbers are treated as 0
- Multiple delimiters can be used together
- Delimiters can be of any length
- The alternate delimiter (if specified) replaces all occurrences of '\n'

## Running Tests

To run the unit tests:

```bash
dotnet test
```

## License

[MIT License](LICENSE) 