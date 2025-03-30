# Inventory Management System API

## Overview
The **Inventory Management System API** is a RESTful service built with **.NET Core** and **MongoDB**. It provides functionalities for managing products, including CRUD operations.

## Features
- Add, update, delete, and retrieve products.
- Uses **MongoDB** for data storage.
- Implements **GUID-based** unique identifiers for products.
- Structured response format with `BaseResponse<T>`.
- Built using **.NET Core Web API**.

## Tech Stack
- **Backend:** .NET Core 8
- **Database:** MongoDB
- **Architecture:** Clean Architecture, Repository Pattern

## Installation
### Prerequisites
Ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community)

### Clone the Repository
```sh
git clone https://github.com/your-repository/inventory-management.git
cd inventory-management
```

### Configuration
1. **Set up MongoDB Connection:**
   - Navigate to `appsettings.json` and update the connection string.
   ```json
   "MongoDB": {
     "ConnectionString": "mongodb://localhost:27017",
     "DatabaseName": "InventoryDB"
   }
   ```

2. **Run MongoDB Locally:**
   ```sh
   mongod --dbpath /your/db/path
   ```

### Running the API
1. Restore dependencies:
   ```sh
   dotnet restore
   ```
2. Build and run:
   ```sh
   dotnet run
   ```
3. The API will be available at:
   ```
   http://localhost:5000/api
   ```

## API Endpoints
### Product Controller
| Method | Endpoint          | Description         |
|--------|------------------|---------------------|
| GET    | `/api/product`   | Get all products   |
| POST   | `/api/product`   | Create a product   |
| PUT    | `/api/product/{id}` | Update a product |
| DELETE | `/api/product/{id}` | Delete a product |

### Example Request - Create Product
```http
POST /api/product
Content-Type: application/json

{
 
  "Name": "Red Bull Blueberry 8.4oz",
  "Category": "Energy Drinks",
  "Price": 100,
  "Quantity": 10
}
```

### Example Response
```json
{
    "result": {
        "id": "67e963580d507f64fe175c9c",
        "name": "Red Bull Blueberry 8.4oz"
    },
    "status": "Success",
    "statusCode": 201,
    "message": "Product created successfully."
}
```

## License
This project is licensed under the [MIT License](LICENSE).

