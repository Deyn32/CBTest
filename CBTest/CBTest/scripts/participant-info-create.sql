CREATE TABLE ParticipantInfo (_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
							  Name TEXT NOT NULL,
							  RegionName TEXT,
							  Status TEXT NOT NULL,
							  Address TEXT,
							  Country TEXT,
							  DateIn DATE NOT NULL
							  BIC TEXT NOT NULL);

CREATE TABLE Account (_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
							  DateIn DATE NOT NULL,
							  Status TEXT NOT NULL,
							  CBRBIC TEXT NOT NULL,
							  Number TEXT NOT NULL,
							  CK TEXT NOT NULL,
							  Type TEXT NOT NULL);