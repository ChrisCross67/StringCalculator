Feature: Calculator
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
	In order to avoid silly mistakes
	As a math idiot
	I *want* to be told the **sum** of ***two*** numbers

Link to a feature: [Calculator](SpecFlowProject1/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: An empty string will return 0
	Given the input is ""
	When I calculate the sum
	Then the result should be "0"

Scenario Outline: Single input should return the number
  Given the input is <input>
  When I calculate the sum
  Then the result should be <result>

  Examples:
    | input		| result	|
    |	"1"		|   "1"		|
    |	"1.1"	|   "1.1"	|


Scenario Outline: Multiple mumbers sould return their sum
  Given the input is <input>
  When I calculate the sum
  Then the result should be <result>

  Examples:
    | input			| result	|
    |	"1.1,2.2"	|   "3.3"	|
    |	"1,2,3,4,5"	|   "15"	|
	
#Scenario: Allow the add method to handle newlines as separators
#	Given the input is "1\n2,3"
#	When I calculate the sum
#	Then the result should be "6"

#Scenario: Add should return the message 'Number expected but \n found at position 6. if input is 175.2,\n35
#	Given the input is "175.2,\n35"
#	When I calculate the sum
#	Then the result should contains "Number expected but "
#	And the result should contains "\n' found at position 6"

Scenario: Add should return 'Number expected but EOF found' when the input ends with a separator
	Given the input is "1,3,"
	When I calculate the sum
	Then the result should contains "Number expected but EOF found"

#Scenario Outline: Allow to add custom separator
#  Given the input is <input>
#  When I calculate the sum
#  Then the result should be <result>
#
#  Examples:
#    | input			    | result	                                    |
#    |	"//;\n1;2"	    |   "3"	                                        |
#    |	"//|\n1|2|3"	|   "6"	                                        |
#    |	"//|\n1|2|3"	|   "6"	                                        |
#    |	"//sep\n2sep3"	|   "5"	                                        |
#    |	"//|\n1|2,3"	|   "'|' expected but ',' found at position 3"	|

Scenario Outline: Negative number not allowed
  Given the input is <input>
  When I calculate the sum
  Then the result should contains <result>

  Examples:
    | input			| result							|
    |	"-1,2"		|   "Negative not allowed : -1"		|
    |	"2,-4,-5"	|   "Negative not allowed : -4, -5"	|

Scenario: Add should return multiple errors if they are multiple errors
	Given the input is "-1,,2"
	When I calculate the sum
	Then the result should contains "Negative not allowed : -1"
	And the result should contains "Number expected but ',' found at position 3"