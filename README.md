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
cd ChallengeCalculator.Console
dotnet run
```

## Input Formats

The calculator supports various input formats:

1. Basic format with comma delimiter:
```
1,2,3,4
```

2. Numbers with newline delimiter:
```
1\n2,3
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
- Negative numbers are not allowed (will throw an exception)
- Invalid numbers are treated as 0
- Multiple delimiters can be used together
- Delimiters can be of any length

## Running Tests

To run the unit tests:

```bash
dotnet test
```

## License

[MIT License](LICENSE) 