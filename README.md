# 🧮 WPF Calculator Application

## 🌟 Overview
This project is a **WPF-based Calculator** application developed using **C# and .NET**, inspired by the Windows Calculator. It includes two main modes:
- **🧾 Standard Mode**: Supports basic arithmetic operations.
- **💻 Programmer Mode**: Displays number representations in different bases (HEX, DEC, OCT, BIN) while calculations remain in decimal.

## 🔥 Features
- **➕ Arithmetic Operations**: Addition (+), Subtraction (-), Multiplication (*), Division (/), Modulus (%), Square Root (√), Square (x²), Inversion (1/x), and Negation (+/-).
- **🧠 Memory Functions**:
  -  **MC (Memory Clear)**: Clears stored memory.
  -  **M+ (Memory Add)**: Adds current display value to memory.
  -  **M- (Memory Subtract)**: Subtracts the display value from memory.
  -  **MR (Memory Recall)**: Retrieves the stored value.
  -  **MS (Memory Store)**: Stores the current value in memory.
  -  **M> (Memory Stack)**: Displays stored values for reuse.
- **✍️ Editing Functions**:
  -  **Backspace**: Deletes the last entered digit.
  -  **CE (Clear Entry)**: Clears the last input but retains previous calculations.
  -  **C (Clear All)**: Resets the entire calculation.
- **📋 Clipboard Support**:
  - ✂️ Cut, 📄 Copy, 📌 Paste (manually implemented with string operations instead of default textbox methods).
- **🔢 Digit Grouping**: Formats numbers based on locale settings (e.g., `1.000` for Romanian settings, `1,000` for UK settings).
- **⚙️ Programmer Mode**:
  - Converts numbers between **Binary (BIN), Octal (OCT), Decimal (DEC), and Hexadecimal (HEX)**.
  - Input is limited to characters specific to the selected base.
- **⌨️ Keyboard & Mouse Input**: Users can interact via keyboard shortcuts or mouse clicks.
- **🔒 Non-Editable Display**: Ensures users cannot manually modify results.
- **💾 Persistent Settings**:
  -  **Digit Grouping Preference**
  -  **Last Used Mode (Standard/Programmer)**
  -  **Last Selected Base in Programmer Mode**
- **📜 Help Menu**:
  -  **File Menu**: Includes Cut, Copy, Paste, and Digit Grouping options.
  -  **Help Menu**: Provides an **About** section displaying the developer's name and group.

## 📸 Screenshots
### 🧮 Standard Mode
![Standard](https://github.com/user-attachments/assets/42212582-24ed-48de-8522-88025faf8581)
### 💻 Programmer Mode
![programer-mode](https://github.com/user-attachments/assets/dcdf20a2-a524-431c-8931-47bb77cf7e33)
### 🔄 Mode Selection
![change-mode](https://github.com/user-attachments/assets/862a2885-6989-44e5-8dd8-e4a97c21dbc6)
### ℹ️ About Window
![credits](https://github.com/user-attachments/assets/4ec33ac3-a9d8-49de-ae23-f13d1b47797c)
### 📜 Menu Options
![menu](https://github.com/user-attachments/assets/87d3a2da-4d40-4fb0-b627-e1fc82b056cd)

## ⚙️ Implementation Details
### 🛠️ Technologies Used
- ** WPF (.NET 6/7)**
- ** C#** for backend logic
- ** XAML** for UI design

### 🛡️ Exception Handling
-  **Invalid Inputs**: Users are prevented from entering characters not supported by the selected mode/base.
-  **Division by Zero**: Displays an appropriate error message instead of crashing.
-  **Continuous Calculations**: Allows users to chain operations, e.g., `2 + 3 - 4` updates intermediate results dynamically.
-  **ESC Key**: Clears all operations (same as `C` button).
-  **ENTER Key**: Evaluates the current expression.

### 💾 Data Persistence
User preferences (Digit Grouping, Last Used Mode, Last Selected Base) are stored using **.NET Settings API** or encoded file storage.

## 🚀 How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/kiprinel05/Windows-Calculator.git
   cd Windows-Calculator
   ```
2. Open the project in **Visual Studio**.
3. Ensure you have **.NET 6/7 SDK** installed.
4. Run the application.

## 🌟 Future Improvements
-  **Scientific Mode**: Support for trigonometric and logarithmic functions.
-  **History Panel**: Display previous calculations.
-  **Customizable Themes**: Light/Dark mode support.

## 👨‍💻 Developer Information
**🆔 Name:** Dumitrasc Ciprian  
🔗 [Visit Other Projects](https://github.com/kiprinel05)

