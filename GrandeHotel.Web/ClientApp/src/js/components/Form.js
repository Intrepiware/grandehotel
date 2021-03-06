import React, { Component } from "react";
import ReactDOM from "react-dom";

export default class Form extends Component {
    constructor() {
        super();
        this.state = { value: "" };
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        const { value } = event.target;
        this.setState(() => {
            return { value };
        });
    }

    render() {
        return (
            <React.Fragment>
                <form>
                    <input type="text" value={this.state.value} onChange={this.handleChange} />
                </form>
                <p>
                    Hi, my name is <b>{this.state.value}</b>.
                </p>
            </React.Fragment>
        )
    }
}

const wrapper = document.getElementById("container");
wrapper ? ReactDOM.render(<Form />, wrapper) : false;

