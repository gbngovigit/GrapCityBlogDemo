# GrapCityBlogDemo  
   Default Login Seeded User Cred:
   email : "Test@grapecity.com" Pass: "Test@1234"
# Run App
1. Pleaase update the 'Default ConnestionString' in appsettings.json
2. Run 'Update-Database'  through Nudget PackageManger Console Cmd for (Infrastruture project) or Run Migaration
3. Run App (IIS Express) -> Navigati to http://localhost:{port}/index.html 
4. Swagger page will open for action.
5. For Auth in Swagger UI i.e.: Bearer {TokenStrin}

# Concepts Used
1. CQRS Design Pattern with MedaitR
2. Fluent Validation
3. Ef Core framework for code first approch
4. ApiAuthorizationDbContext For Api Auth flow
5. AutoMapper
6. Linq
7. Jwt Bearer Authentication for resource access
8. CustomExceptionHandlerMiddlewareExtensions Middleware for exception handling. 