import React from "react";

const ItemListTopic = (props) => {
    return (
        <li style="display:flex">
            <h1>{props.id}</h1>
            <h1>{props.title}</h1>
            <h1>{props.topicType}</h1>
            <h1>{props.summary}</h1>
            <a type="button" onClick={() => { props.updateTopic(props.id); }}>Изменить</a>
            <a type="button" onClick={() => { props.deleteTopic(props.id); }}>Удалить</a>
        </li>
    );
}

export default ItemListTopic;