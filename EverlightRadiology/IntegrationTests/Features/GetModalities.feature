Feature: GetModalities

Feature to test the Get Modalities API

@tag1 @api
Scenario: Verify status success for valid details
	Given I hit the GetModalities endpoint with valid headers
	When I get the response back from GetModalities Api
	Then I will recieve an 'OK' response
	And I expect a status code of: 200

@tag2 @api
Scenario: Verify response schema for valid response
	Given I hit the GetModalities endpoint with valid headers
	When I get the response back from GetModalities Api
	Then the response received would pass the schema check for GetModalities Api

@tag3 @api
Scenario: Verify response nodes for valid response
	Given I hit the GetModalities endpoint with valid headers
	When I get the response back from GetModalities Api
	Then the value of node '[0].code' is 'MR'
	And the value of node '[2].name' is 'Xray'
