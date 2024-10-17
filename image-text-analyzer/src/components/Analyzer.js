import React, { useState } from 'react';
import axios from 'axios';

const Analyzer = () => {
    const [url, setUrl] = useState('');
    const [images, setImages] = useState([]);
    const [wordCount, setWordCount] = useState({});

    const handleAnalyze = async () => {
        try {
            debugger;
            const response = await axios.post('http://localhost:5108/api/analyze/extract', { url });
            setImages(response.data.Images);
            setWordCount(response.data.WordCount);
        } catch (error) {
            console.error("Error analyzing URL", error);
        }
    };

    return (
        <div>
            <h1>Image and Text Analyzer</h1>
            <input
                type="text"
                placeholder="Enter URL"
                value={url}
                onChange={(e) => setUrl(e.target.value)}
            />
            <button onClick={handleAnalyze}>Analyze</button>

            <h2>Images</h2>
            <div className="image-gallery">
                {images.map((imgSrc, index) => (
                    <img key={index} src={imgSrc} alt={`img-${index}`} />
                ))}
            </div>

            <h2>Word Count</h2>
            <ul>
                {Object.entries(wordCount).map(([word, count], index) => (
                    <li key={index}>{word}: {count}</li>
                ))}
            </ul>
        </div>
    );
};

export default Analyzer;