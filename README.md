# Project Title

Bug Ticketing System is a web-based application built with ASP.NET Core that helps software development teams efficiently track and manage bugs throughout the software lifecycle. The system supports multiple user roles — Managers, Developers, and Testers — each with specific permissions to report, assign, and resolve bugs.

## API Reference

#### Register User

```http
POST/api/Users/Register
```

| Body       | Type     | Description   |
| :--------- | :------- | :------------ |
| `userName` | `string` | **Required**. |
| `userRole` | `string` | **Required**. |
| `password` | `string` | **Required**. |
| `email`    | `string` | **Required**. |

#### LogIn

```http
POST/api/Users/Login
```

| Body       | Type     | Description   |
| :--------- | :------- | :------------ |
| `userName` | `string` | **Required**. |
| `password` | `string` | **Required**. |

#### Get All projects

```http
GET/api/projects
```

Lists All Projects.

#### Add New Project

```http
POST/api/api/projects
```

| Body                 | Type     | Description   |
| :------------------- | :------- | :------------ |
| `projectName`        | `string` | **Required**. |
| `projectDescription` | `string` | **Required**. |

#### View specific project information and bugs

```http
GET /api/projects/:id
```

| parameter | Type   | Description              |
| :-------- | :----- | :----------------------- |
| `id`      | `Guid` | **Required**. project_id |

#### Report a new bug

```http
POST/api/Bug
```

| Body          | Type     | Description                                             |
| :------------ | :------- | :------------------------------------------------------ |
| `title`       | `string` | **Required**.                                           |
| `description` | `string` | **Required**.                                           |
| `status`      | `string` | **Required**.                                           |
| `priority`    | `string` | **Required**.                                           |
| `project_Id`  | `Guid`   | **Required**. project whic hthe bug will be assigned to |
| `description` | `string` | **Required**.                                           |

#### List all bugs

```http
GET/api/Bug
```

#### View detailed info on a specific bug

```http
GET/api/Bug/{id}
```

| parameter | Type   | Description          |
| :-------- | :----- | :------------------- |
| `id`      | `Guid` | **Required**. Bug_id |

#### Assign a user to a bug

```http
POST/api/Bug/{bug_Id}/assignees
```

| parameter | Type   | Description          |
| :-------- | :----- | :------------------- |
| `bug_Id`  | `Guid` | **Required**. Bug_id |

| Body      | Type     | Description             |
| :-------- | :------- | :---------------------- |
| `user_Id` | `string` | **Required**. user's id |

#### Unassign a user from a bug

```http
DELETE/api/Bug/{bug_Id}/assignees/{user_Id}
```

| parameter | Type   | Description             |
| :-------- | :----- | :---------------------- |
| `bug_Id`  | `Guid` | **Required**. Bug_id    |
| `user_Id` | `Guid` | **Required**. user's id |

#### Upload Attachment: Add an attachment to a bug

```http
POST/api/Bug/{bug_Id}/attachments
```

| parameter | Type   | Description          |
| :-------- | :----- | :------------------- |
| `bug_Id`  | `Guid` | **Required**. Bug_id |

| Body   | Type   | Description   |
| :----- | :----- | :------------ |
| `File` | `File` | **Required**. |

#### Get Attachments for Bug: Retrieve all attachments for a bug

```http
GET/api/Bug/{id}/attachments
```

| parameter | Type   | Description          |
| :-------- | :----- | :------------------- |
| `Id`      | `Guid` | **Required**. Bug_id |

#### Delete Attachment: Remove an attachment from a bug

```http
DELETE/api/Bug/{bug_Id}/attachments/{attachement_Id}
```

| parameter | Type   | Description          |
| :-------- | :----- | :------------------- |
| `bug_Id`  | `Guid` | **Required**. Bug_id |

| Body             | Type   | Description                  |
| :--------------- | :----- | :--------------------------- |
| `attachement_Id` | `Guid` | **Required**. attachement_Id |
