import {useState} from 'react';
import Markdown from 'react-markdown';
import remarkGfm from 'remark-gfm'

export default function Module(props: {
    title: string,
    getter: () => Promise<string>,
    streamGetter?: () => AsyncGenerator<string>
}) {

    const [moduleData, setModuleData] = useState<string>(`${props.title} will be displayed here...`);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    async function getModuleData(): Promise<void> {
        setIsLoading(true);

        if (props.streamGetter) {
            let data = "";
            for await (const chunk of props.streamGetter()) {
                data += chunk;
                setModuleData(data);
            }
            setIsLoading(false);
        } else {
            const result = await props.getter();
            try {
                const parsed = JSON.parse(result);
                setModuleData(parsed.text ?? result);
            } catch {
                setModuleData(result);
            }
        }

        setIsLoading(false);
    }

    return (
        <div className="mb-4">
            <div className="border-bottom pb-2 mb-2 d-flex justify-content-between align-items-center">
                <h2 className="mb-0">{props.title}</h2>
                <button
                    disabled={isLoading}
                    className="btn btn-primary ms-auto d-block"
                    onClick={getModuleData}
                >
                    {isLoading && <span className="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>}
                    <span role="status">{isLoading ? "Loading..." : "Load"}</span>
                </button>
            </div>
            <div className="border rounded p-3 bg-body-tertiary">
                <Markdown remarkPlugins={[remarkGfm]}>{moduleData}</Markdown>
            </div>
        </div>
    );
}
