﻿CREATE TABLE DRONES(
 ID UUID PRIMARY KEY,
 CREATED TIMESTAMP NOT NULL,
 UPDATED TIMESTAMP NOT NULL,
 TAG VARCHAR(50),
 OWNER VARCHAR(50)
);