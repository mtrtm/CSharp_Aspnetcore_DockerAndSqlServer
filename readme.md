# Overview
This project is an empty net core 2.2 web api with a docker-compose that also spins up a linux sql server instance

I have added the AspNetCore.HealthChecks.SqlServer nuget package and configured it to run against the sql server container.  
This demonstrates failure to resolve using the docker name, even though the docker name in the web api container resolves.

# Run
1. Open sln in VS 2019
2. Verify you have docker installed and your hard drive(s) shared
3. Set docker-compose as startup project
4. Run, see that /healthz is "Unhealthy"

# Make health work
1. TL;DR; - set IP to proper internal docker and see it change to "Healthy"
* docker network ls
* docker network inspect <your network here - mine is dockercompose9318822996251545401_default>
* find the ip for sqlserverContainer in output
* set 


# Verify that name resolves to same IP as you set above
1. docker ps
2. docker exec -it <your web api container here - mine is ece485c89701> bash
3. apt-get update
4. apt-get install dnsutils
5. nslookup sqlserverContainer

You will see that it resolves to the same IP that worked in the "Make health work" heading.  For some reason it resolves (and I could add mysql to this project and show you it works with the name instead of the IP) but doesn't work.