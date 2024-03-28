CREATE DATABASE lng2
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
  
USE lng2;

-- Table: Languages
DROP TABLE IF EXISTS Languages;
CREATE TABLE Languages (
    id   INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name TEXT NOT NULL,
    tag  TEXT NOT NULL
);

-- Table: FlashCardAnswers
DROP TABLE IF EXISTS FlashCardAnswers;
CREATE TABLE FlashCardAnswers (
    id         INT PRIMARY KEY AUTO_INCREMENT,
    cardId     INT NOT NULL,
    text       TEXT,
    languageId INT
);

-- Table: FlashCards
DROP TABLE IF EXISTS FlashCards;
CREATE TABLE FlashCards (
    id           INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nextExamDate DATETIME NOT NULL,
    nodeId       INT NOT NULL,
    totalHits    INT DEFAULT 0,
    requiredHits INT DEFAULT 0,
    languageId   INT,
    question     TEXT,
    description  TEXT,
    hitsInRow    INT DEFAULT 0,
    level        NUMERIC DEFAULT 0,
    questPrice   INT DEFAULT 0,
    isCompleted  INT DEFAULT 0
);

-- Table: Nodes
DROP TABLE IF EXISTS Nodes;
CREATE TABLE Nodes (
    id          INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    terrainId   INT NOT NULL,
    name        TEXT,
    description TEXT,
    x           INT NOT NULL,
    y           INT NOT NULL,
    width       INT DEFAULT 0,
    height      INT DEFAULT 0
);

-- Table: ResearchTexts
DROP TABLE IF EXISTS ResearchTexts;
CREATE TABLE ResearchTexts (
    id     INT PRIMARY KEY AUTO_INCREMENT,
    nodeId INT,
    text   TEXT
);

-- Table: Terrains
DROP TABLE IF EXISTS Terrains;
CREATE TABLE Terrains (
    id          INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name        TEXT,
    description TEXT,
    userId      INT NOT NULL DEFAULT 0
);

-- Table: Users
DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
    id           INT PRIMARY KEY AUTO_INCREMENT,
    name         TEXT,
    email        TEXT,
    passwordHash BLOB,
    passwordSalt BLOB,
    refreshToken TEXT,
    tokenCreated DATETIME,
    tokenExpires DATETIME
);
