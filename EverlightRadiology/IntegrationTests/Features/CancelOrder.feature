@Retry
Feature: CancelOrder

Feature to test the Cancel/Delete functionality

@tag1
Scenario: Cancel order successfully
	Given I load the application and click on orders
	When I click on create new
	And I enter a value for all fields
	And I click on submit
	And the user is redirected to orders page
	And the order is successfully created
	And I click on the X button and approve the confirmation popup
	Then the order is successfully cancelled and deleted

@tag2
Scenario: Cancel order but decline confirmation
	Given I load the application and click on orders
	When I select an order to cancel
	And I click on the X button and decline the confirmation popup
	And the user is redirected to orders page
	Then the order is not cancelled
