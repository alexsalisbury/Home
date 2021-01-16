'use strict';
const createError = require('http-errors');
const cookieParser = require('cookie-parser');
const express = require('express');
const path = require('path');
const port = process.env.PORT || 1337;

const app = express();
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');
//app.use(express.json());

//setup public folder
app.use(express.static('assets'));
app.get('/', function (req, res) {
    res.render('home')
});

app.get('/health-check', (req, res, next) => {
    res.sendStatus(200);
});

app.use(function (req, res, next) {
    next(createError(404));
});

// error handler
app.use(function (err, req, res, next) {
    // set locals, only providing error in development
    res.locals.message = err.message;
    res.locals.error = req.app.get('env') === 'development' ? err : {};

    // render the error page
    res.status(err.status || 500);
    res.render('error');
});

app.listen(port, () => console.log(`App started on port ${port}`));