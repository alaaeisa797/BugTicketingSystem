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

#### add(num1, num2)

Takes two numbers and returns the sum.

#### Get All projects.

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

## Documentation

[Documentation](https://linktodocumentation)
