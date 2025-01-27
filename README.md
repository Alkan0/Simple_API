# Business Management API

## Project Overview

This project implements a set of APIs using **C#** and **.NET Core** for managing businesses, contracts, and products. It includes functionalities for creating, updating, and retrieving business information, managing business relationships, and handling contracts and products.

---

## Features

The API provides the following features:

1. **Search Businesses**
   - Search for businesses by:
     - Name
     - Phone number
     - Zip code

2. **Create a New Business**
   - Add a new business with details such as:
     - Name
     - Brand Name
     - Address
     - City
     - Coordinates
     - Zip Code
     - Phone(s)
     - Main and Secondary Activities

3. **Update Business Details**
   - Modify a business's:
     - Phone number(s)
     - Name
     - Address

4. **Business Relationships**
   - Retrieve all relationships of a business.
   - Add a new relationship (e.g., co-location, branch).

5. **Enable or Disable a Business**
   - Activate or deactivate a business.

6. **Manage Contracts and Products**
   - Add a new contract for a business with a specific product.
   - Retrieve all active contracts and associated products for:
     - A specific business
     - All businesses

---

## Data Structure

### Business Entity
- **Fields:**
  - Name
  - Brand Name
  - Address
  - City
  - Coordinates
  - Zip Code
  - Phone(s) (one or more)
  - Main Activity
  - Secondary Activities (one or more)

### Relationships
- Businesses can be related through:
  - Co-location
  - Branch
  - Other types of associations

### Contracts
- **Fields:**
  - Start date
  - End date
  - Associated Product(s)

### Products
- **Fields:**
  - Code
  - Name
  - Activity
  - Price

---

## Requirements

- A business can purchase products only if their activity matches the product's activity.
- Example:
  - A business with the activities "Heating Oil" or "Gas Stations" can only purchase products related to these activities.

---

## Installation and Usage

1. Clone the repository:
   ```bash
   git clone https://github.com/Alkan0/Simple_API.git
