import { useState, useEffect } from 'react'
import axios from 'axios'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'

import Topic from './layout/Topic/CartTopic'
import ListTopics from './layout/ListTopics/ListTopics';
import './App.css'

function App() {
    const [topics, setTopics] = useState([])
    const url = `${baseApiUrl}/getAllTopics`;
    useEffect(() => {
        axios.get(url).then(res => setTopics(res.data));
    }, [])


    const createTopic = (topicTitle, topicType, topicSummary) => {

        const numbers = topics.map(x => x.createTime);

        let maxNumber = 0;//Доработать
        if (numbers.length != 0) {
            maxNumber = Math.max(...numbers);
        }

        const item = {
            id: maxNumber + 1,//Здесь должен быть Guid
            title: topicTitle,
            topicType: topicType,
            summary: topicSummary
        };

        const url = `${baseApiUrl}/topics`;
        axios.post(url, item);

        setTopics([...topics, item]);
    }

    const deleteTopic = (id) => {
        axios.delete(`${baseApiUrl}/topics/${id}`)
        setTopics(topics.filter(item => item.id !== id));
    }
    return (
        <div className="container mt-5">
            <div className="card">
                <div className="card-header">
                    Список тем
                </div>
                <div className="card-body">
                    <ListTopics topics={topics} deleteTopic={deleteTopic} updateTopic={updateTopic} />
                    <ListTopics/>
                </div>
                <div className="card-footer">
                    <AddTopic >
                    </AddTopic>
                    <a type="button" onClick={() => { props.createTopic(props.id); }}>
                    Добавить тему
                    </a>
                </div>
            </div>
        </div>
    )
}

export default App
