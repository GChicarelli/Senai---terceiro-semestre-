CRIAR O BANCO
CREATE DATABASE VH_Burguer;
GO

USE VH_Burguer;
GO
CREATE TABLE Usuario (
	UsuarioID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(60) NOT NULL,
	Email VARCHAR(150) UNIQUE NOT NULL,
	Senha VARBINARY(32) NOT NULL,
	-- Por padrão, o usuário virá ativo
	-- StatusUsuario = 1 (ativo)
	-- StatusUsuario = 0 (inativo)
	StatusUsuario BIT DEFAULT 1
);
GO

CREATE TABLE Produto(
	ProdutoID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100) UNIQUE NOT NULL,
	Preco DECIMAL(10,2) NOT NULL,
	Descricao NVARCHAR(MAX) NOT NULL,
	Imagem VARBINARY(MAX) NOT NULL,
	-- Por padrão, o produto virá ativo
	-- StatusProduto = 1 (ativo)
	-- StatusProduto = 0 (inativo)
	StatusProduto BIT DEFAULT 1,
	-- Chamando a FK de usuario:
	UsuarioID INT FOREIGN KEY REFERENCES Usuario(UsuarioID)
);
GO

CREATE TABLE Categoria(
	CategoriaID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(50) NOT NULL
);
GO

CREATE TABLE ProdutoCategoria(
	-- Chave composta
		-- 1. Criar os atributos
	ProdutoID INT NOT NULL,
	CategoriaID INT NOT NULL,
		-- 2.Aplicar as constraints
	CONSTRAINT PK_ProdutoCategoria PRIMARY KEY (ProdutoID, CategoriaID),
	CONSTRAINT FK_ProdutoCategoria_Produto FOREIGN KEY (ProdutoID) 
		REFERENCES Produto(ProdutoID) ON DELETE CASCADE,
	CONSTRAINT FK_ProdutoCategoria_Categoria FOREIGN KEY (CategoriaID) 
		REFERENCES Categoria(CategoriaID) ON DELETE CASCADE
);
GO

CREATE TABLE Promocao(
	PromocaoID INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100) NOT NULL,
	DataExpiracao DATETIME2(0) NOT NULL, 
	StatusPromocao BIT DEFAULT 1 NOT NULL
);
GO

CREATE TABLE ProdutoPromocao(
	PromocaoID INT NOT NULL,
	ProdutoID INT NOT NULL,
	PrecoAtual DECIMAL(10,2) NOT NULL,

	CONSTRAINT PK_ProdutoPromocao PRIMARY KEY (ProdutoID, PromocaoID),
	CONSTRAINT FK_ProdutoPromocao_Produto FOREIGN KEY (ProdutoID)  
		REFERENCES Produto(ProdutoID) ON DELETE CASCADE,
	CONSTRAINT FK_ProdutoPromocao_Promocao  FOREIGN KEY (PromocaoID) 
		REFERENCES Promocao(PromocaoID) ON DELETE CASCADE
);
GO

CREATE TABLE Log_AlteracaoProduto(
	Log_AltecaoProdutoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME2(0) NOT NULL,
	NomeAnterior VARCHAR (100),
	ValorAnterior DECIMAL(10,2),

	ProdutoID INT FOREIGN KEY REFERENCES Produto(ProdutoID)
);
GO

-- Criando nossas triggers
	--DELETE -> ECLUIR O USUARIO = INATVAR O USUARIO!!! StatusUsuario = 0

CREATE TRIGGER trg_ExclusaoUsuario
ON Usuario
INSTEAD OF DELETE 
AS
BEGIN
	UPDATE a SET StatusUsuario = 0
	FROM Usuario a
	INNER JOIN deleted d
	ON d.UsuarioID =  a.UsuarioID;

END 
GO

-- toda vez que alterarmos a tabela produto = criar um registro na tabela log 

CREATE TRIGGER trg_AlteracaoProduto
ON Produto
AFTER UPDATE
AS
BEGIN
	INSERT INTO Log_AlteracaoProduto(DataAlteracao, ProdutoID, NomeAnterior, ValorAnterior)

	SELECT GETDATE(), ProdutoID, Nome, Preco FROM inserted 

END 
GO

	--DELETE -> ECLUIR O USUARIO = INATVAR O USUARIO!!! StatusUsuario = 0

CREATE TRIGGER trg_ExclusaoProduto
ON Produto
INSTEAD OF DELETE 
AS
BEGIN
	UPDATE a SET StatusProduto = 0
	FROM Produto a
	INNER JOIN deleted d
	ON d.ProdutoID =  a.ProdutoID;

END 
GO

