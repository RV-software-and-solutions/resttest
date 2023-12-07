# RestTest Repository

## Overview

RestTest is a project based on Clean Architecture principles, featuring a generic service for working with graph data structures and integration with various AWS services.

### Synonym Service

The Synonym Service manages synonyms using a graph data structure. Key functionalities include adding synonyms, retrieving synonyms, and managing the graph state.

- **Interface**: [ISynonymService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Application/Services/ISynonymService.cs)
- **Implementation**: [SynonymService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Application/Services/SynonymService.cs)

### Graph Service

#### Overview

The Graph Service in the RestTest repository is a key component designed for managing a graph data structure. It offers a range of functionalities to manipulate and query the graph.

#### IGraphService Interface

The `IGraphService` interface defines the contract for the graph service. It is a generic interface, parameterized for flexibility.

- **File**: [IGraphService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Services/Graph/IGraphService.cs)
- **Key Methods**:
  - `AddVertex`: Adds a vertex to the graph.
  - `AddEdge`: Adds an edge between two vertices.
  - `Search`: Searches for a vertex with a given value.
  - `GetAllConnectedVertices`: Retrieves all vertices connected to a given start vertex.
  - `GetAllVertices`: Retrieves all vertices in the graph.
  - `ClearGraph`: Clears all vertices from the graph.

#### GraphService Implementation

The `GraphService` class implements the `IGraphService` interface, managing the graph's vertices and providing methods to manipulate and query the graph.

- **File**: [GraphService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Services/Graph/GraphService.cs)
- **Features**:
  - **Vertices Management**: Maintains a list of vertices and provides methods to add and delete vertices.
  - **Edge Management**: Methods to add edges, check if an edge exists, and retrieve all connected vertices.
  - **Search Functionality**: Implements depth-first search (DFS) to find vertices.
  - **Graph Initialization and Clearing**: Can load a list of vertices into the graph and clear it.

This service is essential for efficient management and querying of graph-based data structures in the RestTest project.

---

### Graph Models

- **AbstractVertex**: A generic abstract vertex class. [AbstractVertex.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Services/Graph/Models/AbstractVertex.cs)
- **SynonymVertex**: Represents a vertex in a synonym graph. [SynonymVertex.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Domain/Dtos/Synonym/SynonymVertex.cs)

### AWS Integration

- **AWS S3**: Object storage and retrieval. [S3Configuration.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Configuration/S3Configuration.cs), [S3ResourceManager.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Services/ResourceManager/S3/S3ResourceManager.cs)
- **DynamoDB**: NoSQL database services. [AwsDynamoDbConfiguration.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Configuration/AwsDynamoDbConfiguration.cs), [AwsDynamoService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Services/AwsDynamo/AwsDynamoService.cs)
- **AWS Parameter Store**: Application configuration and secrets management. [AwsParameterStoreConfiguration.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Configuration/AwsParameterStoreConfiguration.cs), [AwsParameterStoreManager.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Core/Services/ConfigurationManager/ParameterStore/AwsParameterStoreManager.cs)

### PostgreSQL Database

Configured to work with PostgreSQL for relational data management.

### Testing

Includes both unit and integration tests.

- **Unit Tests**: [SynonymServiceTests.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/tests/Application.UnitTests/Services/SynonymServiceTests.cs)
- **Integration Tests**: [GetResourceItemsWithPaginationTests.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/tests/Application.IntegrationTests/ResourceItems/Queries/GetResourceItemsWithPaginationTests.cs)

---

**Note**: This README is based on the current state of the [RestTest repository](https://github.com/RV-software-and-solutions/resttest) as of the last update and may not reflect the latest changes.
