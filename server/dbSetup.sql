CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        website varchar(255) COMMENT 'User website',
        github varchar(255) COMMENT 'User github',
        linkedin varchar(255) COMMENT 'User linkedin',
        resume varchar(255) COMMENT 'User resume',
        bio varchar(255) COMMENT 'User bio',
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title CHAR(50) NOT NULL,
        subtitle CHAR(50),
        instructions VARCHAR(2000) NOT NULL,
        category CHAR(50) NOT NULL,
        img VARCHAR(360) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT UNIQUE NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name CHAR(50) NOT NULL,
        quantity CHAR(50) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT UNIQUE NOT NULL PRIMARY KEY AUTO_INCREMENT,
        recipeId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        UNIQUE (recipeId, accountId)
    ) default charset utf8 COMMENT '';