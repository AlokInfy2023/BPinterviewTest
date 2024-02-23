Feature: ShoppingMall

@interviewTest
Scenario: Shopping Test
	Given the user navigtes to the shopping website "https://www.saucedemo.com/"
	And the user logs in with the username and password 
	| userName      | password     |
	| standard_user | secret_sauce |
	When the user places all items into the cart
	Then the user moves to the cart section to verify added products
	And user finally checksout and verifies that order was successful 