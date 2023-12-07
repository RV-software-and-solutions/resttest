# RestTest Repository

## Overview

RestTest is a project based on Clean Architecture principles, featuring a generic service for working with graph data structures and integration with various AWS services.

### Synonym Service

The Synonym Service manages synonyms using a graph data structure. Key functionalities include adding synonyms, retrieving synonyms, and managing the graph state.

- **Interface**: [ISynonymService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Application/Services/ISynonymService.cs)
- **Implementation**: [SynonymService.cs](https://github.com/RV-software-and-solutions/resttest/blob/main/src/Application/Services/SynonymService.cs)

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
