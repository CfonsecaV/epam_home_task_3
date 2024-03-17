Feature: ArticleTitleCheck
	In order to get to the right article
	As a specific user
	I want to be able to navigate to a slide and verify the article name

@mytag
Scenario: Go to the Insight menu, select the third article and verify the title matches
	Given I'm on the Epam main page
	When I click on the Insight button
	And I go to the third Slide
	And I click on the read more button
	Then The article name is the same as the one in the slide