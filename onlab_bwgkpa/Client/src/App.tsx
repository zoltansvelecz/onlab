import {Tab, Tabs } from "react-bootstrap";
import Layout from "./components/Layout.tsx";
import {useState} from "react";
import CvEditor from "./components/CvEditor.tsx";
import Overview from "./components/Overview.tsx";
import Interview from "./components/Interview.tsx";


export default function App() {

    const defaultCv = "Name: John Doe\nEmail: johndoe@example.com\nPhone: +1234567890\nAddress: 123 Main Street, City, Country\nSummary: A highly motivated software developer with 5 years of experience in building web applications and a passion for continuous learning.\nSkills: C#, ASP.NET Core, JavaScript, React, SQL, HTML / CSS\nExperience: 1) Software Developer at Tech Solutions Inc. (January 2020 - Present) - Develop and maintain web applications using ASP.NET Core and React.\n2) Junior Developer at Web Innovations Ltd. (June 2017 - December 2019) - Assisted in the development of e - commerce platforms using C# and JavaScript.\nEducation: Bachelor of Science in Computer Science from University Of Technology(Graduation Year: 2017) \nLanguages: English(Fluent), Spanish(Intermediate)";


    

    const [cv, setCv] = useState<string>(defaultCv)

    return (
        <Layout>
            <h1 className="text-center">CV Helper</h1>

            <Tabs
                defaultActiveKey="editor"
                className="mb-3"
            >
                <Tab eventKey="editor" title="Editor">
                    <CvEditor cv={cv} setCv={setCv}></CvEditor>
                </Tab>

                <Tab eventKey="overview" title="Overview">
                    <Overview cv={cv}></Overview>
                </Tab>

                <Tab eventKey="interview" title="Interview">
                    <Interview cv={cv}></Interview>
                </Tab>
            </Tabs>
        </Layout>
    );
}
