
THINGS I WOULD HAVE DONE IN A PRODUCTION PROJECT WHERE SECURITY AND PERFORMANCE ARE A PRIORITY
	- Full CRUD API, it is always useful to have all the endpoints, they would be needed

	- Authenticate the endpoints with token (JWT/Auth0/Okta)

	- Token for API and logging in front end with expiration, and automatically refreshing token if user still connected

	- AWS/Cloud infrastructure for horizontal scaling to keep up with traffic and response times (multiple instances, microservices architecture)

	- Using a NonSQL DB for better response times and to avoid bottlenecks (horizontal scaling and strongly consistency

	- If it had to be a relational DB I would trade off a bit of consistency in order to use features like AWS RDS feature to have parallel instances (e.g. One for writing, 2 for reading)

	- Another alternative for better Relational DB performance could be having multiple synchronized DBs, some for reading and some for writing

	- Distributed cache to improve latency throughout the system

	- Unit test with and 80%/90% coverage



COMMENTS
	- Due to the short ammount of time I had (because I had to fly during the weekend) different exception types are not being handle

	- The token is hardcoded in the SignIn response. Again, I did not get to fully implement the token/endpoint authentication

	- I did not get to finish the token authentication between front end and backend

	- Lastly, I could not get to do the unit test.
