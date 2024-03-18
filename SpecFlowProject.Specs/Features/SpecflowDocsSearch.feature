Feature: SpecflowDocsSearch
	In order to find specific documentation about specflow itself
	As a specific user
	I want to be able to navigate to the specflow documentation and search for the specific document


@Specflow
Scenario: Clicking Docs menu option, searching keyword and checking title
	Given I open the official Specflow site
	When I hover the Docs button at the top menu
	And I click con the Specflow documentation option
	And I click on the search bar
	And I input "Installation" into the pop up search bar
	And I select the first result
	Then Then the result page title contains "Installation"
