import React from 'react';
import {Container, Nav, Navbar } from 'react-bootstrap';

export default function Layout(props:React.PropsWithChildren) {
    return (
        <div className="d-flex flex-column min-vh-100">

            <Navbar expand="md" className="bg-body-tertiary">
                <Container>
                    <Navbar.Brand href="/" role="button">HR Assistant</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link href="/">CV Helper</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>

            <main className="flex-grow-1 py-3">
                <Container>
                    {props.children}
                </Container>
            </main>

            <footer className="py-4 text-center text-body-secondary bg-body-tertiary mt-4">
                FullStack lab of <a href="https://portal.vik.bme.hu/kepzes/targyak/VIAUBXAV081-00"
                                 className="link-body-emphasis">BMEVIAUBXAV081</a>
            </footer>
        </div>
    );
}