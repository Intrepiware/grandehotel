import React from 'react';
import 'bulma';
import Nav from './components/Nav';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

import About from './pages/About';
import Login from './pages/Login';

const App = () => {
    return (
        <section className="section">
        <div className="container">
            <Router>
                <Nav />
                <Switch>
                    <Route path="/about" component={About} />
                    <Route path="/login" component={Login} />
                </Switch>
            </Router>
        </div>
        </section>
    )
}

export default App;