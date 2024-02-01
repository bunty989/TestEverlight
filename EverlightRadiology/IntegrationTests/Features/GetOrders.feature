Feature: GetOrders

Feature to test the Get Orders API

@tag1 @api
Scenario: Verify status success for valid details
	Given I hit the GetOrders endpoint with valid headers
	When I get the response back from GetOrder Api
	Then I will recieve an 'OK' response
	And I expect a status code of: 200

@tag2 @api
Scenario: Verify response schema for valid response
	Given I hit the GetOrders endpoint with valid headers
	When I get the response back from GetOrder Api
	Then the response received would pass the schema check for GetOrder Api

@tag3 @api
Scenario: Verify response nodes for valid response
	Given I hit the GetOrders endpoint with valid headers
	When I get the response back from GetOrder Api
	Then the value of node '[1].accessionNumber' is '00487'
	And the value of node '[1].patientMrn' is 'P303'
