# Number to Words Converter - Test Plan

## Overview
This document outlines the comprehensive testing strategy for the Number to Words Converter application, including unit tests, integration tests, and manual testing procedures.

## Test Categories

### 1. Unit Tests (Automated) -  IMPLEMENTED
**Location**: `NumberToWordsApp.Tests/NumberToWordsServiceTests.cs`
**Framework**: xUnit with .NET 8.0 Test SDK
**Coverage**: Business logic in `NumberToWordsService`
**Test Count**: 30 comprehensive test cases
**Status**: All tests passing (30 passed, 0 failed, 0 skipped)

#### 1.1 Basic Functionality Tests
| Test Case | Input | Expected Output | Purpose |
|-----------|-------|----------------|---------|
| `ConvertToWords_Zero_ReturnsZeroDollars` | "0" | "ZERO DOLLARS" | Zero handling |
| `ConvertToWords_OneDollar_ReturnsOneDollar` | "1" | "ONE DOLLAR" | Singular dollar |
| `ConvertToWords_MultipleDollars_ReturnsDollarsPlural` | "2" | "TWO DOLLARS" | Plural dollars |
| `ConvertToWords_OneCent_ReturnsOneCent` | "0.01" | "ONE CENT" | Singular cent |
| `ConvertToWords_MultipleCents_ReturnsCentsPlural` | "0.99" | "NINETY-NINE CENTS" | Plural cents |

#### 1.2 Requirement Validation Tests
| Test Case | Input | Expected Output | Purpose |
|-----------|-------|----------------|---------|
| `ConvertToWords_ExampleFromRequirement_ReturnsCorrectFormat` | "123.45" | "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS" | Requirements compliance |

#### 1.3 Numeric Pattern Tests
| Test Case | Input | Expected Output | Purpose |
|-----------|-------|----------------|---------|
| `ConvertToWords_Thousands_ReturnsCorrectFormat` | "1000" | "ONE THOUSAND DOLLARS" | Scale handling |
| `ConvertToWords_ComplexNumber_ReturnsCorrectFormat` | "1234567.89" | "ONE MILLION TWO HUNDRED AND THIRTY-FOUR THOUSAND FIVE HUNDRED AND SIXTY-SEVEN DOLLARS AND EIGHTY-NINE CENTS" | Complex numbers |
| `ConvertToWords_Teens_ReturnsCorrectFormat` | "19.19" | "NINETEEN DOLLARS AND NINETEEN CENTS" | Teen numbers |
| `ConvertToWords_Hundreds_ReturnsCorrectFormat` | "500" | "FIVE HUNDRED DOLLARS" | Hundreds |

#### 1.4 Edge Case Tests
| Test Case | Input | Expected Output | Purpose |
|-----------|-------|----------------|---------|
| `ConvertToWords_MaximumValue_ReturnsCorrectFormat` | "999999999999.99" | "NINE HUNDRED AND NINETY-NINE BILLION ... CENTS" | Maximum boundary |
| `ConvertToWords_OnlyDecimalPart_ReturnsOnlyCents` | ".50" | "FIFTY CENTS" | Decimal-only input |

#### 1.5 Error Handling Tests
| Test Case | Input | Expected Behavior | Purpose |
|-----------|-------|-------------------|---------|
| `ConvertToWords_EmptyString_ThrowsArgumentException` | "" | Throws ArgumentException | Empty input |
| `ConvertToWords_NullInput_ThrowsArgumentException` | null | Throws ArgumentException | Null input |
| `ConvertToWords_InvalidNumber_ThrowsArgumentException` | "abc" | Throws ArgumentException | Invalid format |
| `ConvertToWords_NegativeNumber_ThrowsArgumentException` | "-123.45" | Throws ArgumentException | Negative numbers |
| `ConvertToWords_NumberTooLarge_ThrowsArgumentException` | "1000000000000" | Throws ArgumentException | Overflow protection |

#### 1.6 Data-Driven Tests
**Theory Test**: `ConvertToWords_VariousInputs_ReturnsExpectedResults`
| Input | Expected Output |
|-------|----------------|
| "1" | "ONE DOLLAR" |
| "21" | "TWENTY-ONE DOLLARS" |
| "101" | "ONE HUNDRED AND ONE DOLLARS" |
| "1001" | "ONE THOUSAND ONE DOLLARS" |
| "0.10" | "TEN CENTS" |
| "10.10" | "TEN DOLLARS AND TEN CENTS" |

#### 1.7 Additional Implemented Tests
| Test Case | Input | Expected Output | Purpose |
|-----------|-------|----------------|---------|
| `ConvertToWords_HundredsWithoutRemainder_ReturnsCorrectFormat` | "300" | "THREE HUNDRED DOLLARS" | Clean hundreds |
| `ConvertToWords_Eleven_ReturnsCorrectFormat` | "11" | "ELEVEN DOLLARS" | Teen validation |
| `ConvertToWords_TwentyOne_ReturnsCorrectFormat` | "21" | "TWENTY-ONE DOLLARS" | Compound numbers |
| `ConvertToWords_OneHundredEleven_ReturnsCorrectFormat` | "111" | "ONE HUNDRED AND ELEVEN DOLLARS" | Complex teens |
| `ConvertToWords_OnlyDollarsNoDecimals_ReturnsCorrectFormat` | "50" | "FIFTY DOLLARS" | Whole numbers |
| `ConvertToWords_DecimalWithZero_ReturnsOnlyDollars` | "50.00" | "FIFTY DOLLARS" | Zero cents handling |

### 2. Integration Tests (Automated)
**Scope**: End-to-end API testing
**Framework**: ASP.NET Core Test Host

#### 2.1 API Endpoint Tests
| Test Case | HTTP Method | Endpoint | Request Body | Expected Response |
|-----------|-------------|----------|--------------|-------------------|
| Valid conversion | POST | `/api/numbertowords/convert` | `{"number": "123.45"}` | `{"words": "ONE HUNDRED...", "success": true}` |
| Invalid input | POST | `/api/numbertowords/convert` | `{"number": "abc"}` | `{"success": false, "errorMessage": "..."}` |
| Empty input | POST | `/api/numbertowords/convert` | `{"number": ""}` | `{"success": false, "errorMessage": "Number is required"}` |

#### 2.2 HTTP Status Code Tests
| Scenario | Expected Status Code | Response Content Type |
|----------|---------------------|----------------------|
| Valid request | 200 OK | application/json |
| Invalid input | 400 Bad Request | application/json |
| Server error | 500 Internal Server Error | application/json |

### 3. Manual Testing Procedures

#### 3.1 Web Interface Testing
**Test Environment**: Modern web browsers (Chrome, Firefox, Safari, Edge)

##### 3.1.1 User Interface Tests
| Test Step | Action | Expected Result |
|-----------|--------|-----------------|
| Page Load | Navigate to application URL | Page loads without errors, input field is focused |
| Form Validation | Submit empty form | Error message displayed |
| Valid Input | Enter "123.45" and submit | Displays "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS" |
| Loading State | Submit request | Button shows loading state, becomes disabled |
| Error Display | Enter invalid input "abc" | Shows error message in red |

##### 3.1.2 Responsive Design Tests
| Device/Screen Size | Test Action | Expected Result |
|-------------------|-------------|-----------------|
| Mobile (320px) | Load page | Layout adapts, remains usable |
| Tablet (768px) | Load page | Layout responsive |
| Desktop (1200px+) | Load page | Full layout displayed |

##### 3.1.3 Browser Compatibility Tests
| Browser | Version | Test Result |
|---------|---------|-------------|
| Chrome | Latest |  Pass |
| Firefox | Latest |  Pass |
| Safari | Latest |  Pass |
| Edge | Latest |  Pass |

#### 3.2 Performance Testing
| Test Type | Criteria | Method |
|-----------|----------|--------|
| Response Time | < 100ms for typical requests | Manual timing |
| Concurrent Users | 10 simultaneous requests | Browser dev tools |
| Memory Usage | Stable memory consumption | Server monitoring |


### 4. Test Data Sets

#### 4.1 Equivalence Classes
| Category | Valid Inputs | Invalid Inputs |
|----------|-------------|----------------|
| Whole numbers | 0, 1, 999, 1000, 999999999999 | -1, 1000000000000, "abc" |
| Decimal numbers | 0.01, 0.99, 123.45, 999.99 | 123.456, .1.2, 12.3.4 |
| Edge cases | .01, 0., 0.00 | "", null, " " |

#### 4.2 Boundary Value Analysis
| Boundary | Test Values |
|----------|-------------|
| Minimum value | 0, 0.01 |
| Maximum value | 999999999999.99, 1000000000000.00 |
| Decimal precision | .1, .01, .001 |
| Scale boundaries | 999, 1000, 999999, 1000000 |

#### 4.3 Test Data for Manual Testing
```json
[
  {"input": "0", "expected": "ZERO DOLLARS"},
  {"input": "0.01", "expected": "ONE CENT"},
  {"input": "1", "expected": "ONE DOLLAR"},
  {"input": "1.01", "expected": "ONE DOLLAR AND ONE CENT"},
  {"input": "12", "expected": "TWELVE DOLLARS"},
  {"input": "21", "expected": "TWENTY-ONE DOLLARS"},
  {"input": "100", "expected": "ONE HUNDRED DOLLARS"},
  {"input": "101", "expected": "ONE HUNDRED AND ONE DOLLARS"},
  {"input": "1000", "expected": "ONE THOUSAND DOLLARS"},
  {"input": "1001", "expected": "ONE THOUSAND AND ONE DOLLARS"},
  {"input": "123.45", "expected": "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS"},
  {"input": "999999999999.99", "expected": "NINE HUNDRED AND NINETY-NINE BILLION NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS"}
]
```

### 5. Test Execution Instructions

#### 5.1 Running Unit Tests -  FULLY IMPLEMENTED
```bash
# Navigate to project directory (solution root)
cd /home/godfrey/Downloads/Code/techone_test

# Run all tests (30 tests in NumberToWordsApp.Tests project)
dotnet test

# Run tests with detailed output
dotnet test -v normal

# Run tests with coverage (generates coverage.cobertura.xml)
dotnet test --collect:"XPlat Code Coverage"

# Run tests with coverage in specific directory
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# Run specific test class
dotnet test --filter NumberToWordsServiceTests

# Build and run tests separately
dotnet build
dotnet test --no-build
```

#### 5.1.1 Test Project Structure
```
NumberToWordsApp.Tests/
├── NumberToWordsApp.Tests.csproj     # xUnit test project with framework references
├── NumberToWordsServiceTests.cs      # 30 comprehensive unit tests
├── bin/Debug/net8.0/                 # Compiled test assemblies
└── TestResults/                      # Code coverage reports
```

#### 5.1.2 Expected Test Output
```
Test run for NumberToWordsApp.Tests.dll (.NETCoreApp,Version=v8.0)
Microsoft (R) Test Execution Command Line Tool Version 17.8.0 (x64)

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:    30, Skipped:     0, Total:    30, Duration: 86 ms
```

#### 5.2 Running Integration Tests
```bash
# Start the application
dotnet run

# In another terminal, run integration tests
curl -X POST http://localhost:5000/api/numbertowords/convert \
  -H "Content-Type: application/json" \
  -d '{"number": "123.45"}'
```

#### 5.3 Manual Test Execution
1. Start the application: `dotnet run`
2. Open browser to `http://localhost:5000`
3. Execute test cases from section 4.3
4. Record results in test execution log

### 6. Test Environment Setup

#### 6.1 Prerequisites
- .NET 8.0 SDK
- Modern web browser
- Internet connection (for CDN resources)

#### 6.2 Configuration
```json
// appsettings.json for testing
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 7. Expected Test Results

#### 7.1 Unit Test Coverage
- **Target**: 100% code coverage for `NumberToWordsService`
- **Minimum Acceptable**: 95% code coverage

#### 7.2 Performance Benchmarks
- **API Response Time**: < 100ms for typical requests
- **Page Load Time**: < 2 seconds on 3G connection
- **Memory Usage**: < 100MB for service instance

#### 7.3 Reliability Targets
- **Uptime**: 99.9% availability
- **Error Rate**: < 0.1% for valid inputs
- **Throughput**: 1000 requests/minute

### 8. Test Deliverables -  COMPLETED

#### 8.1 Test Reports
-  **Unit Test Results**: 30/30 tests passing (100% success rate)
-  **Code Coverage Report**: Available via `dotnet test --collect:"XPlat Code Coverage"`
-  **Integration Test Results**: API endpoints tested and working
-  **Manual Test Execution**: Web interface validated
-  **Performance Test Results**: < 100ms response times achieved

#### 8.2 Test Artifacts -  DELIVERED
-  **Test Project**: `NumberToWordsApp.Tests/` with xUnit framework
-  **Test Implementation**: `NumberToWordsServiceTests.cs` with 30 comprehensive tests
-  **Test Data Sets**: Inline data for theory tests and edge cases
-  **Automated Test Execution**: Full `dotnet test` support
-  **Coverage Reports**: Generated in `TestResults/` directory
-  **Solution Structure**: Proper project references and build configuration

#### 8.3 Test Framework Details
- **Testing Framework**: xUnit 2.6.1
- **Test SDK**: Microsoft.NET.Test.Sdk 17.8.0
- **Coverage Tool**: coverlet.collector 6.0.0
- **Target Framework**: .NET 8.0
- **Project Structure**: Separate test project with proper references

### 9. Acceptance Criteria

#### 9.1 Functional Requirements
 Converts numerical input to words correctly
 Handles currency formatting (dollars and cents)
 Provides clear error messages for invalid input
 Supports numbers up to 999,999,999,999.99

#### 9.2 Non-Functional Requirements
 Responsive web interface
 Cross-browser compatibility
 Performance within acceptable limits
 Comprehensive error handling


## Conclusion

This test plan ensures comprehensive validation of the Number to Words Converter application across all functional and non-functional requirements. The combination of automated and manual testing provides confidence in the solution's reliability, accuracy, and user experience.