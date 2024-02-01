Feature: CreateOrder

Feature to test the create order functionality

@tag1
Scenario: Create order with missing information
	Given I load the application and click on orders
	When I click on create new
	And I click on submit
	Then validation errors are displayed for all fields

@tag2
Scenario: Create order with missing information but valid mrn
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for MRN
	And I click on submit
	Then validation errors are displayed for all fields except mrn

@tag3
Scenario: Entering valid order details removes all validation errors
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for all fields
	Then validation errors are not displayed for any fields

@tag4
Scenario: Entering valid order details and click cancel
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for all fields
	And I click on Cancel
	Then the user is redirected to orders page
	And no order is created

@tag5
Scenario: Create order successfully
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for all fields
	And I click on submit
	Then the order is successfully created
	And the status is automatically set as SC

@tag6
Scenario: Create order successfully with same mrn but different accession number
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for all fields with existing MRN
	And I click on submit
	Then the order is successfully created with both the details

@tag7
Scenario: Create order throws error with same mrn and accession number
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for all fields with existing MRN & accession number
	And I click on submit
	Then the validation error is displayed