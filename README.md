# Link Shortener

How to start in docker compose:

    docker compose up
    
Swagger UI link after starting in docker compose:

    http://localhost:8000/swagger/index.html

Environment variables:

    MySqlConnectionString = mysql connection string
    RedisConnectionString = redis cache connection string
    ShorteningPrice = price per 1 link shortening

Test user (SeedInitialUser migration):

    Login: initialUser
    Password: pass
    
	this user has pregiven 1000 on balance