# 📸 SharePic – Documentation

## 📝 Application Overview

**SharePic** is a web application that allows users to upload and share their photos. Registered users have access to additional features, such as the ability to upvote or downvote photos uploaded by others.

---

## 👤 User Guide

### ✅ Registration & Login

- You must be logged in to vote on photos.
- You can register or log in using the built-in authentication system.

### 📤 Uploading a Photo

1. Click on the **"Upload"** button in the menu.
2. A photo upload form will appear.
3. Enter a **title** for your photo.
4. Choose an image by:
   - **Dragging and dropping** it into the designated area, or
   - **Clicking** on the area to select a file from your device.
5. Click the **"Add the photo"** button.

Once the photo is successfully uploaded, you’ll be redirected to the photo’s detail page, where you can:

- Copy the shareable link;
- Upload another photo;
- Vote (👍 or 👎) for the image.

### 👍👎 Voting on Photos

- Each photo can be voted on using the **like** or **dislike** buttons.
- Only logged-in users can vote.

---

## 🗃️ Database Description

The application uses the built-in **ASP.NET Identity** authentication system, which includes the following default tables:

- `AspNetUsers`
- `AspNetRoles`
- `AspNetUserRoles`
- `AspNetUserClaims`
- `AspNetUserLogins`

### 📷 Custom Tables

#### `Photos`

| Column     | Type         | Description                    |
|------------|--------------|--------------------------------|
| `Id`       | `int`        | Primary Key                    |
| `Title`    | `string`     | Title of the photo             |
| `ImagePath`| `string`     | Path to the uploaded image     |

#### `Votes`

| Column     | Type         | Description                            |
|------------|--------------|----------------------------------------|
| `PhotoId`  | `int`        | Foreign key to the photo               |
| `VoteId`   | `int`        | Primary Key                            |
| `VoteType` | `enum`       | Indicates vote type (upvote/downvote)  |

---

> ℹ️ *This documentation is intended to provide an overview of SharePic’s functionality and database structure.*

