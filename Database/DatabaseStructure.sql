-- SQLite
-- DROP TABLE IF EXISTS Games;
CREATE TABLE IF NOT EXISTS Games
    (
        Id integer primary key AUTOINCREMENT,
        Name VARCHAR(100) NOT NULL,
        Description VARCHAR(1000) NULL,
        Image VARCHAR(512) NULL,
        Grade NUMBER DEFAULT 0
    );