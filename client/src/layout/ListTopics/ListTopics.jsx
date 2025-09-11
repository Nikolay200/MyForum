import React from "react";
import ItemListTopic from "./components/ItemListTopic";


const ListTopics = (props) => {

    return (
        <ul>
            <li>
                {
                    props.topics.map(
                        item => (
                            <ItemListTopic
                                key={item.id}
                                id={item.id}
                                title={item.title}
                                topicType={item.topicType}
                                summary={item.summary}
                                updateTopic={props.updateTopic}
                                deleteTopic={props.deleteTopic} />)
                    )
                }
            </li>
        </ul>
    );
};

export default ListTopics;