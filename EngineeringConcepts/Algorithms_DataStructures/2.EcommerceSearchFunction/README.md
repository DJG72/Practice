# Exercise 2: E-commerce Platform Search Function

## 👨‍💻 Developer Info
- **Name**: DEBJIT GHOSH
- **Assignment**: Cognizant Digital Nurture 5.0  
- **Skill**: Data Structures and Algorithms  

---

## 🧠 Problem Statement
You are working on the search functionality of an e-commerce platform.  
The search needs to be optimized for **fast performance**.

---

## ✅ Objectives

- Implement **Linear Search** and **Binary Search** on a list of products.
- Understand and apply **asymptotic notation (Big O)**.
- Compare performance of both search algorithms.

---

## 🏗️ Implementation Details

### 👨‍🔧 Class Used
- `Product`: productId, productName, category

### 🔎 Features
- `LinearSearch` method (O(n))
- `BinarySearch` method (O(log n)) on sorted product array

---

## 📊 Time Complexities

| Operation       | Best Case     | Average Case  | Worst Case   |
|----------------|---------------|---------------|--------------|
| Linear Search   | O(1)          | O(n/2)        | O(n)         |
| Binary Search   | O(1)          | O(log n)      | O(log n)     |

> Binary Search is faster for large sorted datasets.

---

## 📸 Output Screenshot

Below is the sample run showing both searches in action:

![Output](./Output/Output.png)

---

## 🛠️ How to Run

```bash
cd Algorithms_DataStructures/2.EcommerceSearchFunction/Code
dotnet run