#!/bin/bash

// MongoDB is fundamentally designed for "create on first use"
// If you do not insert data with your JavaScript files, then no database is created.
db.createCollection("default");
