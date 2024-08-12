import { useState} from 'react';
import axios from 'axios';

const Generate = () => {
    const [text, setText]=useState('');

    const onGenerateClick = () => {
        window.location.href = `/api/file/generate?amount=${text}`;
    };
    
    return (
        <div className='container' style={{ marginTop: 60 }}>
            <div className='d-flex vh-100' style={{ marginTop: -70 }}>
                <div className='d-flex w-100 justify-content-center align-self-center'>
                    <div className='row'>
                        <input type='text' onChange={e=>setText(e.target.value)} className='form-control-lg' placeholder='Amount'></input>
                    </div>
                    <div className='row'>
                        <div className='col-md-4 offset-md-2'>
                            <button onClick={onGenerateClick} className='btn btn-primary btn-lg'>Generate</button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    )

}
export default Generate;