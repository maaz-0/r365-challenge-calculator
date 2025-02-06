# R365 Challenge Calculator

A .NET Core console application that performs string calculations based on various input formats.

## Architecture

The application follows the Facade design pattern to provide a simplified interface to the calculator system:

- `ICalculatorFacade` - Provides a unified interface for calculator operations
- `CalculatorFacade` - Implements the facade, coordinating between Parser and Calculator
- `Calculator` - Handles the core mathematical operations
- `Parser` - Processes input strings and extracts numbers

### Benefits
- Simplified client code
- Better encapsulation of calculator logic
- Clean separation of concerns
- Improved testability
- Centralized configuration management

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

## Project Structure

- `ChallengeCalculator.Console`: Console application project
  - `Program.cs`: Main entry point and UI logic
- `ChallengeCalculator.Core`: Core business logic
  - `CalculatorFacade.cs`: Main facade for the calculator system
  - `Calculator.cs`: Core calculation logic
  - `Parser.cs`: Input string parsing
- `ChallengeCalculator.Tests`: Unit tests
  - Comprehensive tests for all components

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

## Using the Calculator

The calculator supports four operations:
1. Addition (+)
2. Subtraction (-)
3. Multiplication (*)
4. Division (/)

When you run the application:
1. Select an operation by entering its number (1-4)
2. Enter your numbers using any of the supported delimiter formats
3. View the result
4. Repeat or press Ctrl+C to exit

### Input Format Examples

1. Basic comma-separated numbers:
```
1,2,3,4
```

2. Using newline delimiter (\\n):
```
1\n2,3\n4
```

3. Single custom delimiter:
```
//#\n1#2#3
```

4. Custom delimiter of any length:
```
//[***]\n1***2***3
```

5. Multiple custom delimiters:
```
//[*][!!][r9r]\n1*2!!3r9r4
```

### Operation-Specific Examples

1. Addition:
```
Input: 1,2,3,4
Output: 1 + 2 + 3 + 4 = 10

Input: //[+][==]\n1+2==3
Output: 1 + 2 + 3 = 6
```

2. Subtraction (requires exactly two numbers):
```
Input: 10,3
Output: 10 - 3 = 7

Input: //[***]\n10***3
Output: 10 - 3 = 7
```

3. Multiplication:
```
Input: 2,3,4
Output: 2 * 3 * 4 = 24

Input: //[##]\n2##3##4
Output: 2 * 3 * 4 = 24
```

4. Division (requires exactly two numbers):
```
Input: 10,2
Output: 10 / 2 = 5

Input: //[div]\n10div2
Output: 10 / 2 = 5
```

## Command-line Arguments

The application supports several command-line arguments to customize its behavior:

### 1. Alternate Delimiter (--alt-delimiter)
Specify a different delimiter instead of '\\n':
```bash
# Use pipe as delimiter
dotnet run --alt-delimiter="|"
Input: 1|2,3|4
Output: 1 + 2 + 3 + 4 = 10

# Use dollar sign
dotnet run --alt-delimiter="$"
Input: 1$2,3$4
Output: 1 + 2 + 3 + 4 = 10
```

### 2. Allow Negative Numbers (--allow-negative)
Enable support for negative numbers:
```bash
dotnet run --allow-negative
Input: 1,-2,3,-4
Output: 1 + -2 + 3 + -4 = -2
```

### 3. Custom Upper Bound (--upper-bound)
Set maximum valid number (numbers above this are treated as 0):
```bash
dotnet run --upper-bound=500
Input: 1,501,2,499,3
Output: 1 + 0 + 2 + 499 + 3 = 505
```

### Combining Arguments
You can combine multiple arguments:
```bash
dotnet run --alt-delimiter="|" --allow-negative --upper-bound=500
Input: 1|-2|501|3
Output: 1 + -2 + 0 + 3 = 2
```

### Special Characters as Delimiters
When using special characters (|, &, *, etc.) as delimiters, wrap them in quotes:
```bash
dotnet run --alt-delimiter="|"    # Use pipe
dotnet run --alt-delimiter="&"    # Use ampersand
dotnet run --alt-delimiter="$"    # Use dollar sign
```

## Error Handling

The calculator handles various error cases:

1. Invalid numbers are treated as 0:
```
Input: 1,abc,2,xyz,3
Output: 1 + 0 + 2 + 0 + 3 = 6
```

2. Numbers above upper bound are treated as 0:
```
Input: 1,1001,2,3
Output: 1 + 0 + 2 + 3 = 6
```

3. Negative numbers (when not allowed):
```
Input: 1,-2,3
Error: Input includes negative numbers: -2
```

4. Division by zero:
```
Input: 10,0
Error: Cannot divide by zero
```

5. Wrong number of operands for subtraction/division:
```
Input: 1,2,3
Error: Subtract operation requires exactly two numbers
```

## Running Tests

The project includes comprehensive unit tests for all components:

```bash
dotnet test
```

Key test areas:
- Basic mathematical operations
- Input parsing with various delimiters
- Error handling and validation
- Configuration options
- Facade pattern integration
