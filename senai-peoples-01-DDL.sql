CREATE DATABASE M_Peoples;
GO

USE M_Peoples;
GO

CREATE TABLE Funcionarios
(
	idFuncionario INT PRIMARY KEY IDENTITY
	, Nome VARCHAR(200)
	, Sobrenome VARCHAR(200)
);
GO