# Plan Your Activity

## Generate db migration
~~~bash
cd PlanYourActivity
~~~
~~~bash
# install
dotnet tool install --global dotnet-ef
~~~
~~~bash
# update
dotnet tool update --global dotnet-ef
~~~
~~~bash
# create migration
dotnet ef migrations add Initial --project App.DAL.EF --startup-project WebApp --context ApplicationDbContext
~~~
~~~bash
# apply migration
dotnet ef database update --project App.DAL.EF --startup-project WebApp --context ApplicationDbContext 
~~~

## generate rest controllers

### Add nuget packages
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer
~~~bash
# install
dotnet tool install --global dotnet-aspnet-codegenerator
~~~
~~~bash
# update
dotnet tool update --global dotnet-aspnet-codegenerator
~~~
~~~bash
cd WebApp
# MVC
dotnet aspnet-codegenerator controller -m Group -name GroupsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
# Rest API
dotnet aspnet-codegenerator controller -m Group -name GroupsController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
~~~


## Generate Identity UI

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f 
~~~

~~~bash
dotnet aspnet-codegenerator controller -m Public.DTO.v1.Gender -name GenderController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name EventController -m App.Domain.Event -actions -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~
