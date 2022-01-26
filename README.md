
THINGS I WOULD HAVE DONE IN A PRODUCTION PROJECT WHERE SECURITY AND PERFORMANCE ARE A PRIORITY
	- Full CRUD API, it’s always good to have all the endpoints, they would be need it
	- Authenticate the endpoints through token to give access to use them (JWT/Auth0/Okta)
	- Token for API and logging in front end with expiracy time and autorenewal
	- AWS/Cloud infrastructure for horizontal scaling to keep up with traffic and response times (multiple instances, microservices architecture)
	- Using a NonSQL DB for better response times and to avoid bottlenecks (horizontal scaling and strongly consistent)
	- If it had to be a relational DB I would trade off a bit of consistence in order to use features like AWS RDS feature to have parallel instances (e.g. One for writing, 2 for reading)
	- Another alternative for better Relational DB performance could be having multiple synchronized DBs, some for reading and some for writing
	- Distributed cache for improved time responses regarding DB access
	- Unitest with and 80%/90% coverage
	- API responses a bit more polished
	- More comments/documentation



ALCARACIONES
	- DB Script in the repo creates the db, and add the testing data
	- Because of time exception types are not being handle (only one catch)
	- The token is hardcoded in the SignIn response, didn’t get to fully implement the token/endpoint authentication
	- I hashed all the data in the Created Users table, I believe is a good practice to hash all sensible/personal information
	- I didn’t know if I was allowed to use EF, so I used plain ADO because EF is an ORM and the document said not to use ORMs.
	- Tested with the data in the DBscript
