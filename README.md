# MarktGuru
Develop a .NET API project (preferably .NET Core) to manage products with typical CRUD operations, allowing users to create, update, and remove products in a database.
![image](https://github.com/user-attachments/assets/cd542f9e-f019-4467-9d85-37acb0e627c9)

## Requirments
- [x] Authentication: For this demo project, you can hard-code the username and password
within the system. Database access isn’t necessary for authentication in this case (consider
usage by web and native clients). [Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/1)
- [x] Product List: Retrieves a list of products including ID, Name, Availability, and Price. This
should be accessible to anonymous users as well. [Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/2)
- [x] Product Details by ID: Returns details of a product, including ID, Name, Availability, Price,
Description, and DateCreated, available to anonymous users.[Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/3)
- [x] Add Product: Allows authenticated users to add a new product to the system. Ensure that
duplicate product names are not allowed. [Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/4)
- [x] Update Product: Enables authenticated users to update product information. [Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/5)
- [x] Delete Product: Allows authenticated users to remove a product from the system. [Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/6)

Dev => Master [Pull Request] (https://github.com/gyanverma2/MarktGuru/pull/7)

## Tech-Stack To Understand
<ul>
  <li>.Net Core Rest API</li>
  <li>Web API</li>
  <li>Dependancy Injection</li>  
  <li>Entity Framework Core Integration</li>
  <li>SQLite Integration</li>
  <li>MediatR CQRS</li>
  <li>Jwt Authentication</li>
  <li>Swagger</li>
  <li>Mock & Unit Testing</li>
</ul>

## Folder & File Structure
<ul>
  <li>MarktGuru.Products.Infrastructure : Implement DbContext, Migration Script, Entity Configuration</li>
  <li>MarktGuru.Products.Domain: Defines Entity defination of database</li>
  <li>MarktGuru.Products.Common: Implement all common logic which can be shared by other projects easily. e.g Excpetions, Middleware, Wrapper</li>
  <li>MarktGuru.Products.Application: Implement application logic with CQRS patter to intract with database</li>
  <li>MarktGuru.Products.Api: Api project to get start everything for REST API, Swagger, Routing & Configuration</li>
  <li>Test - Folder contains all the unit test projects</li>
  <li>ci.yml - File build project and run all test in build process after creating pull request</li>
 </ul>

 ### Data Models - MarktGuru.Products.Domain
 <ul>
  <li>Models\Product.cs - Model for product details</li>
  <li>Models\ProductPrice.cs - Model supporting product price</li>
</ul>
  
### Database - SQLite
 <ul>
  <li>MarktGuru.Products.Infrastructure\Context - IProductDbContext.cs - Entity framework supported context intract with database</li>
  </ul>
  
  services.AddDbContext<ProductDbContext>(options =>
    {
        options.UseSqlite(configuration.GetConnectionString("SqlLightConnectionString"));
    });
    
### Version & Depedancy 
 <ul>
  <li>Microsoft.EntityFrameworkCore</li>
  <li>Microsoft.EntityFrameworkCore.SqlLight</li>
  <li>Microsoft.AspNetCore.Mvc.Testing </li>
  <li>Microsoft.AspNetCore.Http</li>
  <li>MediatR </li>
  <li>FluentValidation.AspNetCore</li>
  <li>FluentValidation </li>
  <li>xunit</li>
  <li>Swashbuckle.AspNetCore</li>
 </ul>
    
### Scope For Improvement
 <ul>
  <li>Audit Trail for Add/Update/Delete operation</li>
  <li>Entra login (SSO login) or oauth2 authentication</li>
  <li>Redis Catch imeplementation (I do not have any active server)</li>
  <li>Code Cleanup and duplicate code removal</li>
  <li>SonarQube Analysis</li>
  <li>Improve product price based on source system e.g web, mobile, tablet or any similar business logic</li>
 </ul>

 Thanks