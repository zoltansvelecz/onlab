import {
    BoldItalicUnderlineToggles,
    MDXEditor,
    toolbarPlugin,
    UndoRedo,
    Separator,
    BlockTypeSelect,
    ListsToggle,
    DiffSourceToggleWrapper,
    diffSourcePlugin,
    StrikeThroughSupSubToggles,
    headingsPlugin,
    listsPlugin,
    quotePlugin,
    thematicBreakPlugin
} from '@mdxeditor/editor'
import '@mdxeditor/editor/style.css'

export default function CvEditor(props: { cv: string, setCv: (cv: string) => void }) {

    const plugins = [headingsPlugin(), listsPlugin(), quotePlugin(), thematicBreakPlugin(), diffSourcePlugin(), toolbarPlugin({
        toolbarContents: () => (
            <DiffSourceToggleWrapper options={['rich-text', 'source']}>
                <UndoRedo/>
                <Separator/>
                <BoldItalicUnderlineToggles/>
                <Separator/>
                <StrikeThroughSupSubToggles/>
                <Separator/>
                <ListsToggle/>
                <Separator/>
                <BlockTypeSelect/>
            </DiffSourceToggleWrapper>
        )
    })];

    return (
        <div className="border border rounded">
            <MDXEditor markdown={props.cv} plugins={plugins} onChange={props.setCv}/>
        </div>
    );
}
