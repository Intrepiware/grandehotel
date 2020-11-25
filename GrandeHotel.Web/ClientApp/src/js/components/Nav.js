import React, { useContext } from 'react';
import { Link } from 'react-router-dom';

function Nav() {
    const token = false;

  return (
    <nav className="navbar" role="navigation" aria-label="main navigation">
      <div className="navbar-brand">
        <Link className="navbar-item" to="/">
          <img src="https://bulma.io/images/bulma-logo.png" width="112" height="28" />
        </Link>
      </div>
      <div className="navbar-menu">
        <div className="navbar-start">
          <Link to="/about" className="navbar-item">
            About
          </Link>
          <Link to="/shop" className="navbar-item">
            Shop
          </Link>
          <Link to="/about" className="navbar-item">
            About
          </Link>
        </div>
        <div className="navbar-end">
          {!token && <Link to="/login" className="navbar-item">
            Login
          </Link>}
          {!!token && <Link to="/logout" className="navbar-item">
            Logout
          </Link>}
        </div>
      </div>
    </nav>
  );
}

export default Nav;
