Feature: PostOrders

Feature to test the Post Orders API

@tag1 @api
Scenario: Verify status success for valid details
	Given I modify the body for dynamic Accession Number
	And I hit the PostOrders endpoint with valid headers
	When I get the response back from PostOrder Api
	Then I will recieve an 'Created' response
	And I expect a status code of: 201

@tag1 @api
Scenario Outline: Verify 400 error messages for missing mandatory body
	Given I modify the body for missing '<nodeName>' from body
	And I hit the PostOrders endpoint with invalid body
	When I get the response back from PostOrder Api
	Then I expect a status code of: 400
	And the response received would pass the 400 schema check for PostOrder Api
	And the value of node 'status' is '400'
	And the value of node 'title' is 'One or more validation errors occurred.'
	And the value of node '<errorNodeType>' is '<errorMessage>'

Examples:
	| nodeName         | errorNodeType              | errorMessage                            |
	| accessionNumber  | errors.AccessionNumber[0]  | The AccessionNumber field is required.  |
	| patientMrn       | errors.PatientMrn[0]       | The PatientMrn field is required.       |
	| patientFirstName | errors.PatientFirstName[0] | The PatientFirstName field is required. |
	| patientLastName  | errors.PatientLastName[0]  | The PatientLastName field is required.  |
	| orgCode          | errors.OrgCode[0]          | The OrgCode field is required.          |
	| modality         | errors.Modality[0]         | The Modality field is required.         |
	#| siteId           | errors.SiteId[0]           | The SiteId field is required.           |
	#| studyDateTime    | errors.StudyDateTime[0]    | The StudyDateTime field is required.    |

@tag2 @api
Scenario: Verify api throws 409 error for duplicate Accession Numbers
	Given I create a new order
	And I hit the PostOrders endpoint with valid headers
	When I get the response back from PostOrder Api
	Then I expect a status code of: 409
	And the response received would pass the 409 schema check for PostOrder Api

@tag3 @api
Scenario: Verify response nodes for duplicate Accession Numbers
	Given I hit the PostOrders endpoint with valid headers
	When I get the response back from PostOrder Api
	Then the value of node 'status' is '409'
	And the value of node 'detail' is 'An order already exists with accession number [jhjyui]'