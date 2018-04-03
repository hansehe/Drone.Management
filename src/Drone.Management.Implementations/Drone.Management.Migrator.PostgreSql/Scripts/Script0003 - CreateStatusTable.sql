﻿CREATE TABLE STATUSES(
 ID UUID PRIMARY KEY,
 CREATED TIMESTAMP NOT NULL,
 STATUS VARCHAR(50),
 OPERATIONAL BOOLEAN,
 FK_DRONEID UUID REFERENCES DRONES(ID)
);