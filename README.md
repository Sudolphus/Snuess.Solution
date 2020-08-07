# Dr. Snuess's Factory
## By Micheal Hansen, 8.7.2020

_An app to view a list of engineers and machines in the fantabulous factory of Doctor Snuess_

## Setup Instructions
1. To run this program, you'll need to have the [.NET framework installed](https://dotnet.microsoft.com/download/dotnet-core/2.2)
2. You'll also need to have MySQL Server installed [Mac](https://dev.mysql.com/downloads/file/?id=484914)/[Windows](https://dev.mysql.com/downloads/file/?id=484919)
3. You'll also have to acquire the repo, by either clicking the download button, or running `git clone https://github.com/Sudolphus/Snuess.Solution.git` in a git-enabled terminal
4. While MySQL is running, enter the following commands in the terminal:
```
DROP DATABASE  IF EXISTS `micheal_hansen`
CREATE DATABASE  IF NOT EXISTS `micheal_hansen`
USE `micheal_hansen`;
DROP TABLE IF EXISTS `engineermachine`;
CREATE TABLE `engineermachine` (
  `EngineerMachineId` int NOT NULL AUTO_INCREMENT,
  `EngineerId` int NOT NULL,
  `MachineId` int NOT NULL,
  PRIMARY KEY (`EngineerMachineId`),
  KEY `IX_EngineerMachine_EngineerId` (`EngineerId`),
  KEY `IX_EngineerMachine_MachineId` (`MachineId`),
  CONSTRAINT `FK_EngineerMachine_Engineers_EngineerId` FOREIGN KEY (`EngineerId`) REFERENCES `engineers` (`EngineerId`) ON DELETE CASCADE,
  CONSTRAINT `FK_EngineerMachine_Machines_MachineId` FOREIGN KEY (`MachineId`) REFERENCES `machines` (`MachineId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
DROP TABLE IF EXISTS `engineers`;
CREATE TABLE `engineers` (
  `EngineerId` int NOT NULL AUTO_INCREMENT,
  `FirstName` longtext,
  `LastName` longtext,
  `FullName` longtext,
  PRIMARY KEY (`EngineerId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
DROP TABLE IF EXISTS `machines`;
CREATE TABLE `machines` (
  `MachineId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext,
  PRIMARY KEY (`MachineId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
```

5. Alternately, if you have MySql Workbench, you can import the schema from the micheal_hansen.sql file included in the Snuess.Solution folder.
6. Alternately, alternately, the migrations are included within the project, and can be automatically installed by running `dotnet ef database update` from the Factory directory.
7. In the appsettings.json file, you'll need to update the "PWD" entry to your MySql Server password.
8. In your terminal, navigate to the Snuess.Solution/Factory directory, and enter commands `dotnet restore` and `dotnet run`; the project should then be viewable in your web browser at `http://localhost:5000/`

## Known Bugs & Support Info
_No bugs are currently known, but please reach out via my GitHub account should you encounter any._

