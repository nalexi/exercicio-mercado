CREATE TABLE supermercados (
    id INT PRIMARY KEY IDENTITY(1,1),
    cnpj VARCHAR(20),
    nome VARCHAR(100),
    faturamento DECIMAL,
    registro_ativo bit
),

CREATE TABLE produtos(
    id INT PRIMARY KEY IDENTITY(1,1),
    id_supermercado INT NOT NULL,
    FOREIGN KEY id_supermercado REFERENCES supermercados(id),
    id_fornecedor INT NOT NULL,
    FOREIGN KEY id_fornecedor REFERENCES fornecedores(id),
    nome VARCHAR(100),
    peso FLOAT,
    preco DECIMAL,
    registro_ativo bit
),

CREATE TABLE fornecedores(
    id INT PRIMARY KEY IDENTITY(1,1),
    razao_social VARCHAR(100),
    nome_fantasia VARCHAR(100),
    inscricao_estadual VARCHAR(100),
    registro_ativo bit
),