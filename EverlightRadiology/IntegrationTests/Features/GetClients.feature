@Retry
Feature: GetClients

Feature to test the Get Client API

@tag1 @api
Scenario: Verify status success for valid details
	Given I hit the GetClient endpoint with valid headers
	When I get the response back from GetClient Api
	Then I will recieve an 'OK' response
	And I expect a status code of: 200

@tag2 @api
Scenario: Verify response schema for valid response
	Given I hit the GetClient endpoint with valid headers
	When I get the response back from GetClient Api
	Then the response received would pass the schema check for GetClient Api

@tag3 @api
Scenario: Verify response nodes for valid response
	Given I hit the GetClient endpoint with valid headers
	When I get the response back from GetClient Api
	Then the value of node '[2].orgCode' is 'USC'
	And the value of node '[1].sites[2].id' is '203'
